using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace GasStationRatingSystem.Web.Reports
{
    public partial class GetVisitInfoXtraReport
    {
        public GetVisitInfoXtraReport()
        {
            InitializeComponent();
            subreport1.ReportSource = new GasStationRatingSystem.Web.Reports.VisitAnswerOptionXtraReport(Guid.Empty);

        }

        private void subreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            XRSubreport subreport = sender as XRSubreport;
          
            if (Guid.TryParse(this.GetCurrentColumnValue("VisitId").ToString(), out Guid VisitId))
            {
                 VisitAnswerOptionXtraReport OrdersProductClientXtraReport = new VisitAnswerOptionXtraReport(VisitId);
                subreport.ReportSource = OrdersProductClientXtraReport;

            }
        }
        public class GetStationDTO
        {
            public Guid StationId { get; set; }
            public Guid VisitId { get; set; }
            public string StationName { get; set; }

        }
    }
}
