

using _1CommonInfrastructure.Models;
using _2DataAccessLayer.Interfaces;
using _3BusinessLogicLayer.Interfaces;

namespace _3BusinessLogicLayer.Services
{
    public class UnitService :  IUnitService
    {
        private readonly IUnitDal _UnitDal;
        //private readonly IUnitBalService _UnitBalService;
        public UnitService(IUnitDal UnitDal
        //ILoggingService loggingService,
        //IUnitDal UnitDal,
        //IAuditDal auditDal
       // IUnitBalanceService balsvc
        ) 
        {
            _UnitDal = UnitDal;
            // _UnitBalService = balsvc;
        }

        public async Task<UnitModel?> GetById(int UnitId)
        {           
            return _UnitDal.GetById(UnitId);
        }

        public async Task<List<UnitModel>> GetAll()
        {            
            return _UnitDal.GetAll();
        }

        public async Task<int> CreateUnit(UnitModel Unit)
        {
            //write validations here
            var newUnitId = _UnitDal.CreateUnit(Unit);
            return newUnitId;
        }

        public async Task UpdateUnit(UnitModel Unit)
        {
            //write validations here 
            _UnitDal.UpdateUnit(Unit);
        }

        public async Task DeleteUnit(int UnitId)
        {            
            try
            {
                //if(balservice.getBal(UnitId) = 0)
                _UnitDal.DeleteUnit(UnitId);
            }
            catch (Exception e)
            {
                //_loggingService.WriteLog(LoggingLevel.Error, "Layer", $"Error delete Unit Id:{UnitId}. {e.Message}", e.StackTrace);
            }
        }
    }
}
