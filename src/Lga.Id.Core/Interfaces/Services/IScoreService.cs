using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Core.Services.ServiceWrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lga.Id.Core.Interfaces.Services
{

    /*
    There should be these endpoints:
    1. `GET /Scores` - gets all Scores.    
    2. `GET /Scores/{id}` - gets the project that matches the specified score id 
    3. `POST /Scores` - creates a new score.
    4. `PUT /Scores/{id}` - updates a score.
    5. `DELETE /Scores/{id}` - deletes a score and its scoredetails.
    6. `GET /Scores/{ id}/scoredetails` - finds all scoredetails for a specified score.
    7. `GET /Scores/{id}/scoredetails/{optionId}` - finds the specified score option for the specified score.
    8. `POST /Scores/{id}/scoredetails` - adds a new score option to the specified Score.
    9. `PUT /Scores/{ id}/scoredetails/{optionId}` - updates the specified score detail.
    10. `DELETE /Scores/{id}/scoredetails/{optionId}` - deletes the specified score detail.
     */

    public interface IScoreService
    {
        Task<IEnumerable<Score>> GetAllScores();
        //Task<Score> GetScoreById(int scoreId);
        Task<ScoreResponse> GetScoreResponseById(int scoreId);
        Task<ScoreResponse> AddScore(Score newScore);
        Task<ScoreResponse> UpdateScore(int scoreId, Score updateScore);
        Task<ScoreResponse> DeleteScore(int scoreId);
        Task<IEnumerable<ScoreDetail>> GetAllScoreDetailsByScoreId(int scoreId);
        Task<ScoreDetailResponse> GetScoreDetailByScoreDetailId(int scoreId, int scoreDetailId);
        Task<ScoreDetailResponse> AddScoreDetailByScoreId(int scoreId, ScoreDetail newScoreDetail);
        Task<ScoreDetailResponse> UpdateScoreDetailByScoreId(int scoreId, ScoreDetail updatedScoreDetail);
        Task<ScoreDetailResponse> DeleteScoreDetail(int scoreId, int scoreDetailId);

    }
}
