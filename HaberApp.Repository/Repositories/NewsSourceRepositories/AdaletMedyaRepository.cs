using HaberApp.Core.Models;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Repositories;
using HaberApp.Core.Repositories.NewsSourceRepositories;
using HaberApp.Core.Utils;
using HtmlAgilityPack;
using System.Web;

namespace HaberApp.Repository.Repositories.NewsSourceRepositories
{
    public class AdaletMedyaRepository : IAdaletMedyaRepository
    {
        private readonly INewsRepository newsRepository;
        private readonly IImageHelperService imageHelperService;
        public AdaletMedyaRepository(INewsRepository newsRepository, IImageHelperService imageHelperService)
        {
            this.newsRepository = newsRepository;
            this.imageHelperService = imageHelperService;
        }
        public async Task GetArticleSourceListAsync(string categorySourceUrl, int CategoryID, CancellationToken cancellationToken = default)
        {
            var doc = new HtmlDocument();
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    var document = await client.GetAsync(categorySourceUrl);
                    if (!document.IsSuccessStatusCode)
                    {
                        return;
                    }

                    doc.LoadHtml(await document.Content.ReadAsStringAsync());

                }
            }
            catch (Exception)
            {
                return;
            }

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//article[@class='tek_genis_fotolu_by_category1']//a");
            if (nodes != null)
            {
                var list = nodes.Reverse();


                foreach (var node in list)
                {
                    AddArticleToDbAsync(node.Attributes["href"]?.Value, CategoryID).Wait(cancellationToken);
                }


            }
        }

        public async Task AddArticleToDbAsync(string articleSourceUrl, int CategoryID, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    var document = await client.GetAsync(articleSourceUrl);
                    if (!document.IsSuccessStatusCode)
                    {
                        return;
                    }

                    var doc = new HtmlDocument();
                    doc.LoadHtml(await document.Content.ReadAsStringAsync());

                    HtmlNode nodeTitle = doc.DocumentNode.SelectSingleNode("//h1[@class='hbaslik pdlr-20']");

                    if (nodeTitle == null)
                    {
                        nodeTitle = doc.DocumentNode.SelectSingleNode("//div[@class='yazarin_yazi_basligi']");
                    }
                    if (nodeTitle == null)
                    {
                        return;
                    }
                    var title = HttpUtility.HtmlDecode(nodeTitle.InnerText);
                    title = !string.IsNullOrEmpty(title) ? title.Trim() : title;
                    if (!newsRepository.HasArticle(title, cancellationToken).Result)
                    {
                        var article = new News();
                        article.NewsTitle = title;
                        article.SeoUrl = StringHelper.FriendlySeoUrl(title);
                        HtmlNode subDesc = doc.DocumentNode.SelectSingleNode("//p[@class='lead hs-head-font']");
                        if (subDesc != null)
                        {
                            article.NewsSubTitle = HttpUtility.HtmlDecode(subDesc.InnerText);
                        }
                        else
                        {
                            article.NewsSubTitle = title;
                        }

                        HtmlNode nodeContent = doc.DocumentNode.SelectSingleNode("//div[@class='icerik_detay']");
                        if (nodeContent != null)
                        {
                            HtmlNodeCollection reklamNode = nodeContent.SelectNodes("//div[@class='reklam']");
                            if (reklamNode != null)
                            {
                                reklamNode.ToList().ForEach(node =>
                                {
                                    node.Remove();
                                });
                            }
                            article.NewsContent = HttpUtility.HtmlDecode(nodeContent.InnerHtml);
                        }

                        HtmlNode pictureNode = doc.DocumentNode.SelectSingleNode("//div[@class='onecikan_gorsel']//img");
                        //if (pictureNode != null)
                        //{
                        //    var picUrl = pictureNode.Attributes["data-lazy-src"]?.Value;
                        //    var fileExt = Path.GetExtension(picUrl);
                        //    var fileName = seoUrl + fileExt;
                        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                        //    var imageBytes = await client.GetByteArrayAsync(picUrl);
                        //    await File.WriteAllBytesAsync(filePath, imageBytes);
                        //    article.NewsPicture = fileName;
                        //}


                        if (pictureNode != null)
                        {
                            var responseModel = await imageHelperService.ImageResult(pictureNode.Attributes["data-lazy-src"].Value);
                            if (responseModel != null && responseModel.success)
                            {
                                var variants = imageHelperService.RestoreVariants(responseModel);

                                article.NewsPictureSmall = variants[0];
                                article.NewsPictureMedium = variants[1];
                                article.NewsPictureBig = variants[2];
                            }
                        }

                        article.CategoryId = CategoryID;
                        article.SourceUrl = articleSourceUrl;
                        article.NewsSource = NewsSource.AdaletMedya;


                        await this.newsRepository.CreateAsync(article, cancellationToken);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }


        }
    }


}

