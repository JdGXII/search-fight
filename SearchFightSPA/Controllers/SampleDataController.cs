using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Output;
using Services.Interfaces;

namespace SearchFightSPA.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private ISearchService _searchService;
        private IComparisonService _comparisonService;

        public SampleDataController(ISearchService searchService, IComparisonService comparisonService )
        {
            _searchService = searchService;
            _comparisonService = comparisonService;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<QueryResult>> GetAllResults(List<string> searchTerms)
        {
            _searchService.SearchQueries = searchTerms;
            var googleResults = await _searchService.PerformGoogleSearch();
            var bingResults = await _searchService.PerformBingSearch();
            var winners = _comparisonService.GetAllSearchResults(new List<List<QueryResult>>() { googleResults, bingResults }, searchTerms);

            return winners;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<QueryResult>> GetWinners(List<string> searchTerms)
        {
            _searchService.SearchQueries = searchTerms;
            var googleResults = await _searchService.PerformGoogleSearch();
            var bingResults = await _searchService.PerformBingSearch();
            var winners = _comparisonService.GetWinners(new List<List<QueryResult>>() { googleResults, bingResults }, searchTerms);

            return winners;
        }
    }
}
