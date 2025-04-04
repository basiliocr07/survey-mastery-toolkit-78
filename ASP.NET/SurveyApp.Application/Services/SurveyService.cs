
using SurveyApp.Application.Interfaces;
using SurveyApp.Domain.Models;
using SurveyApp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return await _surveyRepository.GetAllAsync();
        }

        public async Task<Survey?> GetSurveyByIdAsync(int id)
        {
            return await _surveyRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateSurveyAsync(Survey survey)
        {
            return await _surveyRepository.AddAsync(survey);
        }

        public async Task<bool> UpdateSurveyAsync(Survey survey)
        {
            return await _surveyRepository.UpdateAsync(survey);
        }

        public async Task<bool> DeleteSurveyAsync(int id)
        {
            return await _surveyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Survey>> GetSurveysByStatusAsync(string status)
        {
            var surveys = await _surveyRepository.GetAllAsync();
            return status.ToLower() switch
            {
                "active" => surveys.Where(s => s.Status == "active"),
                "draft" => surveys.Where(s => s.Status == "draft"),
                "archived" => surveys.Where(s => s.Status == "archived"),
                _ => surveys
            };
        }

        public async Task<bool> SendSurveyEmailsAsync(int surveyId, List<string> emailAddresses)
        {
            // In a real implementation, this would connect to an email service
            // For now, we just return true to simulate success
            var survey = await _surveyRepository.GetByIdAsync(surveyId);
            if (survey == null || emailAddresses.Count == 0)
                return false;

            // Email sending logic would go here
            return true;
        }
    }
}
