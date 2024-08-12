
using DataAccessLibrary.Models;
namespace DataAccessLibrary.Data;
public class PeopleData : IPeopleData
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public PeopleData(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public async Task<IEnumerable<PeopleModel>> GetAllPeople()
    {
        var output = await _sqlDataAccess.LoadDateAsync<PeopleModel, dynamic>(
            "dbo.spPeople_GetAll",
            new { }
            );

        return output;
    }

    public async Task UpdatePerson(PeopleModel person)
    {
        await _sqlDataAccess.SaveDateAsync("dbo.spPeople_Update", person);
    }

    public async Task InsertPerson(PeopleModel person)
    {
        await _sqlDataAccess.SaveDateAsync("dbo.spPeople_Insert", new{ person.FirstName, person.LastName });
    }

    public async Task DeletePerson(int id)
    {
        await _sqlDataAccess.SaveDateAsync("dbo.spPeople_Delete", new { Id = id });
    }
}

