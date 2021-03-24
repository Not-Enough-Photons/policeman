using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(Policeman.BuildInfo.Description)]
[assembly: AssemblyDescription(Policeman.BuildInfo.Description)]
[assembly: AssemblyCompany(Policeman.BuildInfo.Company)]
[assembly: AssemblyProduct(Policeman.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + Policeman.BuildInfo.Author)]
[assembly: AssemblyTrademark(Policeman.BuildInfo.Company)]
[assembly: AssemblyVersion(Policeman.BuildInfo.Version)]
[assembly: AssemblyFileVersion(Policeman.BuildInfo.Version)]
[assembly: MelonInfo(typeof(Policeman.Policeman), Policeman.BuildInfo.Name, Policeman.BuildInfo.Version, Policeman.BuildInfo.Author, Policeman.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "Boneworks")]