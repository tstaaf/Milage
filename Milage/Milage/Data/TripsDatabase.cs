using Milage.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milage.Data
{
    public class TripsDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public TripsDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Trips>().Wait();
        }

        public Task<List<Trips>> GetTripsAsync()
        {
            return _database.Table<Trips>()
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public Task<int> DeleteAllTripsAsync()
        {
            return _database.DeleteAllAsync<Trips>();
        }

        public Task<Trips> GetTripAsync(int id)
        {
            return _database.Table<Trips>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveTripAsync(Trips trip)
        {
            if (trip.ID != 0)
            {
                return _database.UpdateAsync(trip);
            }
            else
            {
                return _database.InsertAsync(trip);
            }
        }
        public Task<int> DeleteTripAsync(Trips trip)
        {
            return _database.DeleteAsync(trip);
        }

        public Task<int> GetTotalDistance()
        {
            var ent = _database.ExecuteScalarAsync<int>("SELECT SUM(Kilometers) FROM Trips");
            return ent;
        }
    }
}
