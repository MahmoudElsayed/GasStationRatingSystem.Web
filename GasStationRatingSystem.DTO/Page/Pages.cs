using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class AsideBarDTO
    {
        public string AreaTitle { get; set; }
        public IEnumerable<AsideBarPagesDTO> AsideBarPagesDTOs { get; set; }
    }
    public class AsideBarPagesDTO
    {
        public string PageTitle { get; set; }
        public string PageRoute { get; set; }
        public bool HasPermission { get; set; }
    }
}