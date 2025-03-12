using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit;

namespace API_Exam.Test1
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private IRepository _repository;

        private readonly LoggerProxy _logger;

        public ArticlesController(IRepository repository, LoggerProxy logger)
        {
            _logger = logger; // use _logger.WriteLine() to write to the console.
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id is not valid.");

            var article = _repository.Get(id);

            if (article == null)
                return NotFound("Article not found.");

            return Ok(article);
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(article.Title))
                    return BadRequest("Title cannot be null, empty, or whitespace.");

                article.Id = _repository.Create(article);
                return Created($"/api/articles/{article.Id}", article);
            }
            catch (Exception ex)
            {
                _logger.WriteLine(ex.Message);
                return BadRequest("Failed to create article.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            try
            {
                var article = _repository.Get(id);

                if (article == null)
                    return NotFound("Article not found.");

                var result = _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.WriteLine(ex.Message);
                return BadRequest("Failed to delete article.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Article articleToUpdate)
        {
            try
            {
                var article = _repository.Get(id);

                if (article == null)
                    return NotFound("Article not found.");

                if (string.IsNullOrWhiteSpace(articleToUpdate.Title))
                    return BadRequest("Title was not provided.");

                _repository.Update(articleToUpdate);

                return Ok(article.Title);

            }
            catch (Exception ex)
            {
                _logger.WriteLine(ex.Message);
                return BadRequest("Failed to update article.");
            }
        }
    }
}

public class LoggerProxy
{
    public LoggerProxy()
    {

    }

    public void WriteLine(string message) => Console.WriteLine(message);
}

public interface IRepository
{
    // Returns a found article or null.
    Article Get(Guid id);
    // Creates a new article and returns its identifier.
    // Throws an exception if a article is null.
    // Throws an exception if a title is null or empty.
    Guid Create(Article article);
    // Returns true if an article was deleted or false if it was not possible to find it.
    bool Delete(Guid id);
    // Returns true if an article was updated or false if it was not possible to find it.
    // Throws an exception if an articleToUpdate is null.
    // Throws an exception or if a title is null or empty.
    bool Update(Article articleToUpdate);
}

public class ArticleRepository : IRepository
{
    private readonly ICollection<Article> _articlesDb;

    public ArticleRepository()
    {
        _articlesDb = new List<Article>();
    }
    public Guid Create(Article article)
    {
        article.Id = Guid.NewGuid();
        _articlesDb.Add(article);
        return article.Id;
    }

    public bool Delete(Guid id)
    {
        var article = Get(id);
        return _articlesDb.Remove(article);
    }

    public Article Get(Guid id)
    {
        return _articlesDb.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Article articleToUpdate)
    {
        var article = _articlesDb.FirstOrDefault(x => x.Id == articleToUpdate.Id);
        article!.Text = articleToUpdate.Text;
        article!.Title = articleToUpdate.Title;
        return true;
    }
}

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}
