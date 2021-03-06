#region License
// ====================================================
// Project Porcupine Copyright(C) 2016 Team Porcupine
// This program comes with ABSOLUTELY NO WARRANTY; This is free software, 
// and you are welcome to redistribute it under certain conditions; See 
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using MoonSharp.Interpreter;
using Newtonsoft.Json.Linq;
using ProjectPorcupine.Localization;

/// <summary>
/// Contains the description of a single overlay type. Contains LUA function name, id and coloring details.
/// </summary>
[MoonSharpUserData]
public class OverlayDescriptor : IPrototypable
{
    public OverlayDescriptor()
    {
        ColorMap = ColorMapOption.Jet;
    }

    /// <summary>
    /// Select the type of color map (coloring scheme) you want to use.
    /// TODO: More color maps.
    /// </summary>
    public enum ColorMapOption
    {
        Jet,
        Random,
        Palette
    }

    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// Gets the localized name.
    /// </summary>
    public string Name
    {
        get { return LocalizationTable.GetLocalization("overlay_" + Type); }
    }

    /// <summary>
    /// Type of color map used by this descriptor.
    /// </summary>
    public ColorMapOption ColorMap { get; private set; }

    /// <summary>
    /// Name of function returning int (index of color) given a tile t.
    /// </summary>
    public string LuaFunctionName { get; private set; }

    /// <summary>
    /// Reads the prototype from the specified JObject.
    /// </summary>
    /// <param name="jsonProto">The JProperty containing the prototype.</param>
    public void ReadJsonPrototype(JProperty jsonProto)
    {
        Type = jsonProto.Name;
        JToken innerJson = jsonProto.Value;
        ColorMap = PrototypeReader.ReadJson(ColorMap, innerJson["ColorMap"]);
        LuaFunctionName = PrototypeReader.ReadJson(LuaFunctionName, innerJson["LuaFunctionName"]);
    }
}
