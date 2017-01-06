using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class SectionResultsDto
    {
        public SectionResultsDto()
        {
            QuestionResults = new List<QuestionResultDto>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Weighting { get; set; }
        public int Rank { get; set; }
        public bool IsActive { get; set; }
        public double ScoreA
        {
            //weighted results score
            get
            {
                if (QuestionResults == null) return 0.0;
                double total_actual = QuestionResults.Sum(q => q.ScoreA);
                double total_possible = QuestionResults.Sum(q => q.effectiveWeight());
                return ((total_actual / total_possible) * Weighting);
            }
        }
        public double ScoreB {
            //weighted results score
            get
            {
                if (QuestionResults == null) return 0.0;
                double total_actual = QuestionResults.Sum(q => q.effectiveScoreB());
                double total_possible = QuestionResults.Sum(q => q.effectiveWeight());
                return ((total_actual / total_possible) * Weighting);
            }
        }
        
        public double SectionPercentageScoreA
        {
            //as a % of for this section only
            get
            {
                if (QuestionResults == null) return 0.0;
                double totalPossible = QuestionResults.Sum(q => q.effectiveWeight());
                double totalScore = QuestionResults.Sum(q => q.ScoreA);
                return (totalScore/totalPossible) * 100;
            }
        }
        public double SectionPercentageScoreB
        {
            //as a % of for this section only
            get
            {
                if (QuestionResults == null) return 0.0;
                double totalPossible = QuestionResults.Sum(q => q.effectiveWeight());
                double totalScore = QuestionResults.Sum(q => q.effectiveScoreB());
                return (totalScore / totalPossible) * 100;
            }
        }
        public List<QuestionResultDto> QuestionResults { get; set; }
    } //end class
}// end namespace