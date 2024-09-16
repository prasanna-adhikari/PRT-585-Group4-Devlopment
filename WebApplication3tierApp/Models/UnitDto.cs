using _1CommonInfrastructure.Models;

namespace WebApplication3tierApp.Models
{
    public class UnitDto
    {
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
                        
    }

    public static class UnitDtoMapExtensions
    {
        public static UnitDto ToUnitDto(this UnitModel src)
        {
            var dst = new UnitDto();
            dst.UnitId = src.UnitId;
            dst.UnitCode = src.UnitCode;
            dst.UnitName = src.UnitName;            
            return dst;
        }

        public static UnitModel ToUnitModel(this UnitDto src)
        {
            var dst = new UnitModel();
            dst.UnitId = src.UnitId;
            dst.UnitCode = src.UnitCode;
            dst.UnitName = src.UnitName;            
            return dst;
        }
    }
}
