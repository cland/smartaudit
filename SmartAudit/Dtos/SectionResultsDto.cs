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
        public double SectionWeightedScore {
            //weighted results score
            get
            {                
                double totalScore = QuestionResults.Sum(q => q.WeightedScore);
                return totalScore * (Weighting/100);
            }
        }
        public double SectionAbsoluteScore
        {
            //weighted results score
            get
            {
                double totalScore = QuestionResults.Sum(q => q.AbsoluteScore);
                return totalScore * (Weighting / 100);
            }
        }
        public double SectionPercentageScore
        {
            //as a % of for this section only
            get
            {
                double totalPossible = QuestionResults.Sum(q => q.QuestionDefinition.Weight);
                double totalScore = QuestionResults.Sum(q => q.WeightedScore);
                return (totalScore/totalPossible) * 100;
            }
        }
        public List<QuestionResultDto> QuestionResults { get; set; }
    } //end class
}// end namespace