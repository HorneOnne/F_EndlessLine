using UnityEngine.SceneManagement;

namespace EndlessLine
{
    public static class Loader
    {
        public enum Scene
        {
            GameplayScene,
        }

        private static Scene targetScene;

        public static void Load(Scene targetScene, System.Action afterLoadScene = null)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Loader.targetScene.ToString());
        }
    }
}
