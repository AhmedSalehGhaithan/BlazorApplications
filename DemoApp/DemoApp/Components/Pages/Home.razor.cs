using DependencyLibrary;
using Microsoft.AspNetCore.Components;

namespace DemoApp.Components.Pages;


public partial class Home
{
    [Inject]
    protected IConfiguration _Config { get; set; } = default;
    [Inject]
    protected IDependencyInBlazer _Depen {  get; set; } = default;
    // functions below
    private string? GetConnectionInfo()=> _Config.GetConnectionString("Default");
    private string ?GetValueInfo(string key) => _Config.GetValue<string>(key);
    private string GetRandom()=> _Depen.GiveRandomNumber();
}