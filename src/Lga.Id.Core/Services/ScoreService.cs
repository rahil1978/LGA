using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Core.Exceptions;
using Lga.Id.Core.Interfaces;
using Lga.Id.Core.Interfaces.Repositories;
using Lga.Id.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Lga.Id.Core.Specifications;
using Lga.Id.Core.Services.ServiceWrappers;
using Lga.Id.Core.Entities.ScoreAggregate;

namespace Lga.Id.Core.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IAsyncRepository<Score> _ScoreRepository;        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<ScoreService> _logger;
        
        public ScoreService(IAsyncRepository<Score> scoreRepository, IUnitOfWork unitOfWork, IAppLogger<ScoreService> logger)
        {
            _ScoreRepository = scoreRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<Score>> GetAllScores()
        {            
            //var ScoreFilterSpecification = new ScoreFilterSpecification();
            var Scores = await _ScoreRepository.ListAllAsync();
            if (Scores == null || Scores.Count <= 0)
            {
                _logger.LogInformation($"No Score is not found");
                return new List<Score>();
            }
            return Scores.OrderBy(x => x.Id).ToList();
        }        
        public async Task<ScoreResponse> GetScoreResponseById(int scoreId)
        {
            var Score = await _ScoreRepository.GetByIdAsync(scoreId);
            if (Score == null)
                return new ScoreResponse("Score not found.");

            return new ScoreResponse(Score);

        }        
        public async Task<ScoreResponse> AddScore(Score score)
        { 
            try
            {
                _logger.LogInformation($"Adding new Score.");
                await _ScoreRepository.AddAsync(score);
                await _unitOfWork.CompleteAsync();
                return new ScoreResponse(score);
            }
            catch (Exception ex)
            {              
                _logger.LogInformation($"{System.DateTime.Now} - Error: Add Score Method failed.");
                return new ScoreResponse($"An error occurred when saving the Score: {ex.Message}");
            }
        }        
        public async Task<ScoreResponse> UpdateScore(int scoreId, Score updatedScore)
        {
            _logger.LogInformation($"Updating Score: {scoreId}.");
            var existingScore = await _ScoreRepository.GetByIdAsync(scoreId);
            if (existingScore == null)
                return new ScoreResponse("Score not found.");
            
            //Id = scoreId;
            existingScore.DisadvantageScore = updatedScore.DisadvantageScore;
            existingScore.AdvantageDisadvantageScore = updatedScore.AdvantageDisadvantageScore;
            existingScore.Year = updatedScore.Year;
            existingScore.Location = updatedScore.Location;
            //existingScore.ScoreDetails = new List<ScoreDetail>(); //to-do

            try
            {
               await  _ScoreRepository.UpdateAsync(existingScore);
               await _unitOfWork.CompleteAsync();

                return new ScoreResponse(existingScore);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: failed to update Score id {updatedScore.Id}.");                
                return new ScoreResponse($"An error occurred when updating the Score: {ex.Message}");
            }
        }
        public async Task<ScoreResponse> DeleteScore(Score score)
        {
            _logger.LogInformation($"Deleting Score: {score.Id}.");            
            try
            {
                await _ScoreRepository.DeleteAsync(score);
                await _unitOfWork.CompleteAsync();
                return new ScoreResponse($"deleted");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: failed to delete Score id {score.Id}.");
                return new ScoreResponse($"An error occurred when updating the Score: {ex.Message}");
            }
        }       
        public async Task<IEnumerable<ScoreDetail>> GetAllScoreDetailsByScoreId(int scoreId)
        {           
            var existingScore = await _ScoreRepository.GetByIdAsync(scoreId);
            if (existingScore == null)
            {
                _logger.LogInformation($"No Score is not found for {scoreId}");
                return new List<ScoreDetail>(); 
            }

           
            return existingScore.ScoreDetails;
        }
        public async Task<ScoreDetailResponse> GetScoreDetailByScoreDetailId(int scoreId, int scoreDetailId)
        {
            var existingScore = await _ScoreRepository.GetByIdAsync(scoreId);
            if (existingScore == null)
            {
                _logger.LogInformation($"No Score is not found for {scoreId}");
                return new ScoreDetailResponse($"Score not found. {scoreId}");
            }
           
            var ScoreDetail = existingScore.ScoreDetails.FirstOrDefault(x => x.Id == scoreDetailId); 
            if(ScoreDetail == null)
            {
                _logger.LogInformation($"No Score Detail is not found for {scoreId}");
                return new ScoreDetailResponse("Score Detail not found.");

            }
            return new ScoreDetailResponse(ScoreDetail);
        }
        public async Task<ScoreDetailResponse> AddScoreDetailByScoreId(int ScoreId, ScoreDetail newDetail)
        {
            _logger.LogInformation($"Adding a new Score Detail: {ScoreId}.");
            var existingScore = await _ScoreRepository.GetByIdAsync(ScoreId);
            if (existingScore == null)
                return new ScoreDetailResponse($"Score {ScoreId} not found ");

            existingScore.AddScoreDetail(existingScore, newDetail.AdvantageDisadvantageDecile, newDetail.DisadvantageDecile, newDetail.IndexOfEconomicResourcesScore,
            newDetail.IndexOfEconomicResourcesDecile, newDetail.IndexOfEducationAndOccupationScore, newDetail.IndexOfEducationAndOccupationDecile,
            newDetail.UsualResedantPopulation);

            try
            {
                await _ScoreRepository.UpdateAsync(existingScore);
                await _unitOfWork.CompleteAsync();

                return new ScoreDetailResponse(newDetail);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: failed to add Scoreion Detail for Score id {ScoreId}.");
                return new ScoreDetailResponse($"An error occurred when adding the Score's Detail: {ex.Message}");
            }
        }
        public async Task<ScoreDetailResponse> UpdateScoreDetailByScoreId(int scoreId, ScoreDetail updatedScoreDetail)
        {
            var existingScore = await _ScoreRepository.GetByIdAsync(scoreId);
            if (existingScore == null)
            {
                _logger.LogInformation($"No Score is not found for {scoreId}");
                return new ScoreDetailResponse($"Score not found. {scoreId}");
            }

            var existingScoreDetail = existingScore.ScoreDetails.FirstOrDefault(x => x.Id == updatedScoreDetail.Id);
            if (existingScoreDetail == null)
            {
                _logger.LogInformation($"No Score Detail is not found for {scoreId}");
                return new ScoreDetailResponse("Score Detail not found.");
            }

            existingScoreDetail.AdvantageDisadvantageDecile = updatedScoreDetail.AdvantageDisadvantageDecile;
            existingScoreDetail.DisadvantageDecile = updatedScoreDetail.DisadvantageDecile;
            existingScoreDetail.IndexOfEconomicResourcesScore = updatedScoreDetail.IndexOfEconomicResourcesScore;           
            existingScoreDetail.IndexOfEconomicResourcesDecile = updatedScoreDetail.IndexOfEconomicResourcesDecile;
            existingScoreDetail.IndexOfEducationAndOccupationScore = updatedScoreDetail.IndexOfEducationAndOccupationScore; ;
            existingScoreDetail.IndexOfEducationAndOccupationDecile = updatedScoreDetail.IndexOfEducationAndOccupationDecile; ;
            existingScoreDetail.UsualResedantPopulation = updatedScoreDetail.UsualResedantPopulation;
            

            try
            {
                await _ScoreRepository.UpdateAsync(existingScore);
                await _unitOfWork.CompleteAsync();

                return new ScoreDetailResponse(existingScoreDetail);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: failed to update Score Detail id {updatedScoreDetail.Id}.");
                return new ScoreDetailResponse($"An error occurred when updating the Score: {ex.Message}");
            }           
            
        }
        public async Task<ScoreDetailResponse> DeleteScoreDetail(int scoreId, int ScoreDetailId)
        {
            var existingScore = await _ScoreRepository.GetByIdAsync(scoreId);
            if (existingScore == null)
            {
                _logger.LogInformation($"No Score is not found for {scoreId}");
                return new ScoreDetailResponse($"Score not found. {scoreId}");
            }

            var existingScoreDetail = existingScore.ScoreDetails.FirstOrDefault(x => x.Id == ScoreDetailId);
            if (existingScoreDetail == null)
            {
                _logger.LogInformation($"No Score Detail is not found for {scoreId}");
                return new ScoreDetailResponse("Score Detail not found.");
            }
           
            existingScoreDetail.IsDeleted = true;           
            try
            {
                await _ScoreRepository.UpdateAsync(existingScore);
                await _unitOfWork.CompleteAsync();

                return new ScoreDetailResponse(existingScoreDetail);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: failed to update Score Detail id {ScoreDetailId}.");
                return new ScoreDetailResponse($"An error occurred when updating the Score: {ex.Message}");
            }
        }

        public async Task<ScoreResponse> DeleteScore(int scoreId)
        {            
            _logger.LogInformation($"Deleting Score: {scoreId}.");
            try
            {
                var existingScore = await _ScoreRepository.GetByIdAsync(scoreId);
                await _ScoreRepository.DeleteAsync(existingScore);
                await _unitOfWork.CompleteAsync();
                return new ScoreResponse($"deleted");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: failed to delete Score id {scoreId}.");
                return new ScoreResponse($"An error occurred when updating the Score: {ex.Message}");
            }
        }
    }
}
