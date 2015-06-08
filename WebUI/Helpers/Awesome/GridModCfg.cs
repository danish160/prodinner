namespace Omu.ProDinner.WebUI.Helpers.Awesome
{
    public class GridModCfg
    {
        private readonly GridModInfo info = new GridModInfo();

        public GridModCfg InfiniteScroll()
        {
            info.InfiniteScroll = true;
            return this;
        }

        public GridModCfg PageInfo()
        {
            info.PageInfo = true;
            return this;
        }

        public GridModCfg PageSize()
        {
            info.PageSize = true;
            return this;
        }

        internal GridModInfo GetInfo()
        {
            return info;
        }
    }
}