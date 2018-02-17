using System.Collections.Generic;

namespace Citesoft.ARAuthoringTool.Wrappers.ARToolKit
{
    public class PreloadedMarker
    {
        public static readonly Dictionary<string, string> markerStore = new Dictionary<string, string>
            {
                {"hiro", "hiro.patt"},
                {"kanji", "kanji.patt"},
                {"aletter", "a.patt"},
                {"bletter", "b.patt"},
                {"cletter", "c.patt"},
                {"dletter", "d.patt"},
                {"fletter", "f.patt"},
                {"gletter", "g.patt"},
                {"sample1", "sample1.patt"},
                {"sample2", "sample2.patt"},
                {"cat", "cat.patt"},
                {"minion", "minion.patt"},
            };
    }
}