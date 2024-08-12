using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IPeopleData
    {
        Task DeletePerson(int id);
        Task<IEnumerable<PeopleModel>> GetAllPeople();
        Task InsertPerson(PeopleModel person);
        Task UpdatePerson(PeopleModel person);
    }
}