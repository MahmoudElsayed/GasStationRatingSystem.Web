using System;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace GasStationRatingSystem.Web.Reports
{
    public partial class VisitAnswerOptionXtraReport
    {
        public VisitAnswerOptionXtraReport(Guid? visitId=null)
        {
            InitializeComponent();
            if (visitId !=Guid.Empty)
            {
                this.sqlDataSource1.Queries[0].Parameters.FirstOrDefault(p => p.Name == "@VisitId").Value = visitId;
            }
        }
    }
}
