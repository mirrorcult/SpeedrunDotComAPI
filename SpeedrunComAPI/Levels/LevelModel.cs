using JetBrains.Annotations;
using SpeedrunComAPI.Categories;
using SpeedrunComAPI.Links;
using SpeedrunComAPI.Variables;

namespace SpeedrunComAPI.Levels;

[PublicAPI]
public struct LevelModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Weblink { get; set; }
    public string Rules { get; set; }
    public LinkModel[] Links { get; set; }
    
    // Embeddable.
    public CategoryListModel[]? Categories { get; set; }    
    // Embeddable.
    public VariableListModel[]? Variables { get; set; }
}

[PublicAPI]
public struct DataLevelModel
{
    public LevelModel Data { get; set; }
}

[PublicAPI]
public struct LevelListModel
{
    public LevelModel[] Data { get; set; }
}