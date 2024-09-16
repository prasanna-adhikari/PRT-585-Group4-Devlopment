using _1CommonInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DataAccessLayer.Interfaces
{
    public interface IUnitDal
    {
        // Getters
        UnitModel? GetById(int UnitId);
        List<UnitModel> GetAll();

        // Actions
        int CreateUnit(UnitModel Unit);
        void UpdateUnit(UnitModel Unit);
        void DeleteUnit(int UnitId);
    }
}
