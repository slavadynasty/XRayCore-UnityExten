using UnityEditor;

public class ForceCompilationUpdate
{
    [MenuItem("Compilation/Request scripts compilation")]
    static void DD()
    {
        UnityEditor.EditorApplication.UnlockReloadAssemblies();
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
    }
}
