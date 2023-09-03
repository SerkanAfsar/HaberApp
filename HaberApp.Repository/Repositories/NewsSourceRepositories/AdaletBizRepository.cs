using HaberApp.Core.Models;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Repositories;
using HaberApp.Core.Repositories.NewsSourceRepositories;
using HaberApp.Core.Utils;
using HtmlAgilityPack;
using System.Web;

namespace HaberApp.Repository.Repositories.NewsSourceRepositories
{
    public class AdaletBizRepository : IAdaletBizRepository
    {
        private readonly INewsRepository newsRepository;
        private readonly ICategoryRepository categoryRepository;

        public AdaletBizRepository(INewsRepository newsRepository, ICategoryRepository categoryRepository)
        {
            this.newsRepository = newsRepository;
            this.categoryRepository = categoryRepository;

        }
        public async Task AddArticleToDbAsync(string articleSourceUrl, int CategoryID, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    var document = await client.GetAsync(articleSourceUrl, cancellationToken);
                    if (!document.IsSuccessStatusCode)
                    {
                        return;
                    }

                    var doc = new HtmlDocument();
                    doc.LoadHtml(await document.Content.ReadAsStringAsync(cancellationToken));

                    HtmlNode nodeTitle = doc.DocumentNode.SelectSingleNode("//h1[@class='title hs-share-title hs-title-font-2']");
                    if (nodeTitle == null)
                    {
                        return;
                    }

                    var title = HttpUtility.HtmlDecode(nodeTitle.InnerText);
                    if (!newsRepository.HasArticle(title, cancellationToken).Result)
                    {
                        var article = new News();
                        article.NewsTitle = title;
                        var categoryEntity = await categoryRepository.GetByIdAsync(CategoryID, cancellationToken);


                        var seoUrl = StringHelper.FriendlySeoUrl(title);

                        HtmlNode subDesc = doc.DocumentNode.SelectSingleNode("//p[@class='lead hs-head-font']");
                        if (subDesc != null)
                        {
                            article.NewsSubTitle = HttpUtility.HtmlDecode(subDesc.InnerText);
                        }
                        else
                        {
                            article.NewsSubTitle = title;
                        }
                        HtmlNode nodeContent = doc.DocumentNode.SelectSingleNode("//div[@id='newsbody']");
                        if (nodeContent != null)
                        {
                            article.NewsContent = HttpUtility.HtmlDecode(nodeContent.InnerHtml);
                        }
                        article.SeoTitle = title;
                        article.SeoDesctiption = title;
                        article.NewsPicture = "deneme";

                        article.SourceUrl = categoryEntity.SeoUrl + "/" + seoUrl;
                        article.CategoryId = CategoryID;
                        article.SourceUrl = articleSourceUrl;
                        article.NewsSource = NewsSource.AdaletBiz;
                        article.ReadCount = 1;
                        article.SeoDesctiption = title;
                        await this.newsRepository.CreateAsync(article, cancellationToken);

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task GetArticleSourceListAsync(string categorySourceUrl, int CategoryID, CancellationToken cancellationToken = default)
        {
            var doc = new HtmlDocument();
            try
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(httpClientHandler))
                {
                    var document = await client.GetAsync(categorySourceUrl, cancellationToken);
                    if (!document.IsSuccessStatusCode)
                    {
                        return;
                    }

                    doc.LoadHtml(await document.Content.ReadAsStringAsync(cancellationToken));

                }
            }
            catch (Exception)
            {
                return;
            }

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='span hs-item hs-beh hs-kill-ml clearfix']//a");
            if (nodes != null)
            {
                var list = nodes.Reverse();

                foreach (var node in list)
                {
                    AddArticleToDbAsync(node.Attributes["href"]?.Value, CategoryID).Wait(cancellationToken);
                }

            }
        }
    }
}
