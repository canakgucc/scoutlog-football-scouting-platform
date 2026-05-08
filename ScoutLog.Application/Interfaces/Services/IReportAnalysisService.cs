using ScoutLog.Application.DTOs.ScoutReports;

namespace ScoutLog.Application.Interfaces.Services;

public interface IReportAnalysisService
{
    ReportAnalysisDto Analyze(string reportText);
}
