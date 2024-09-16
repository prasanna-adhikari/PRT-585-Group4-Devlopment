using _1CommonInfrastructure.Models;
using _2DataAccessLayer.Context;
using _2DataAccessLayer.Context.Models;
using _2DataAccessLayer.Interfaces;
using _2DataAccessLayer.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DataAccessLayer.Services
{
    public class UnitDal : IUnitDal
    {
        //private readonly TestDBEntities context;
        private DBEntitiesContext _db;
        public UnitDal(DBEntitiesContext dbctx)
        {
            this._db = dbctx; // new TestDBEntities();
        }


        public List<UnitModel> GetAll()
        {
            var result = _db.Units.ToList();

            var returnObject = new List<UnitModel>();
            foreach (var item in result)
            {
                returnObject.Add(item.ToUnitModel());
            }

            return returnObject;
        }

        public UnitModel? GetById(int UnitId)
        {
            var result = _db.Units.SingleOrDefault(x => x.UnitId == UnitId);
            return result?.ToUnitModel();
        }


        public int CreateUnit(UnitModel Unit)
        {
            var newUnit = Unit.ToUnit();
            _db.Units.Add(newUnit);
            _db.SaveChanges();
            return newUnit.UnitId;
        }


        public void UpdateUnit(UnitModel Unit)
        {
            var existingUnit = _db.Units
                .SingleOrDefault(x => x.UnitId == Unit.UnitId);

            if (existingUnit == null)
            {
                throw new ApplicationException($"Unit {Unit.UnitId} does not exist.");
            }
            Unit.ToUnit(existingUnit);

            _db.Update(existingUnit);
            _db.SaveChanges();
        }

        public void DeleteUnit(int UnitId)
        {
            var efModel = _db.Units.Find(UnitId);
            _db.Units.Remove(efModel);
            _db.SaveChanges();


        }

    }

}
