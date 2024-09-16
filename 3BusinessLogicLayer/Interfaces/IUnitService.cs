using _1CommonInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _3BusinessLogicLayer.Interfaces
{
    public interface IUnitService
    {
        Task<UnitModel?> GetById(int UnitId);
        Task<List<UnitModel>> GetAll();

        Task<int> CreateUnit(UnitModel Unit);
        Task UpdateUnit(UnitModel Unit);
        Task DeleteUnit(int UnitId);
    }
}
