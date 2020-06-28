namespace yamlist.Modules.Yaml
{
    public static class YamlTransformerSymbols
    {
        public static readonly YamlTransformerSymbol AnchorDecl = new YamlTransformerSymbol(
            @"(:\s*\&)", 
            "_anchor_decl_", 
            @"(_anchor_decl_)", 
            ": &");

        public static readonly YamlTransformerSymbol MergeAnchorDecl = new YamlTransformerSymbol(
            @"(<<\s*:\s*\&)", 
            "_merge_anchor_decl_{0}_", 
            @"(_merge_anchor_decl_\d*_)", 
            "<<: &");

        public static readonly YamlTransformerSymbol AnchorCall = new YamlTransformerSymbol(
            @"(?=[\ \t])(\s*\*)", 
            " _anchor_call_{0}_", 
            @"(_anchor_call_\d*_)", 
            "*");

        public static readonly YamlTransformerSymbol MergeAnchorCall = new YamlTransformerSymbol(
            @"(<<\s*:\s*\*)", 
            "_merge_{0}_: _call_anchor_{0}_", 
            @"(_merge_\d*_:\s*_call_anchor_\d*_)", 
            "<<: *");

    }
}