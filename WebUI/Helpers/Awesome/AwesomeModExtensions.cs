using System;
using System.Collections.Generic;

using Omu.AwesomeMvc;
using Omu.ProDinner.Resources;

namespace Omu.ProDinner.WebUI.Helpers.Awesome
{
    public static class AwesomeModExtensions
    {
        public static AjaxRadioList<T> Odropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            var res = arl.Mod("awem.odropdown");
            var odcfg = new OdropdownCfg();
            odcfg.Caption(Mui.pleaseSelect);

            if (setCfg != null)
            {
                setCfg(odcfg);
            }

            res.Tag(odcfg.ToTag());

            return res;
        }

        public static AjaxRadioList<T> ColorDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.colorDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxRadioList<T> Combobox<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.combobox");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxRadioList<T> ImgDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.imgDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxRadioList<T> ButtonGroup<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.buttonGroupRadio");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxCheckboxList<T> Multiselect<T>(this AjaxCheckboxList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.multiselect");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static MultiLookup<T> DropdownPopup<T>(this MultiLookup<T> multi)
        {
            multi.Tag(new { DropdownPopup = true });
            return multi;
        }

        public static Lookup<T> DropdownPopup<T>(this Lookup<T> lookup)
        {
            lookup.Tag(new { DropdownPopup = true });
            return lookup;
        }

        public static Grid<T> Mod<T>(this Grid<T> grid, Action<GridModCfg> setCfg = null)
        {
            if (setCfg != null)
            {
                var cfg = new GridModCfg();
                 setCfg(cfg);
                var info = cfg.GetInfo();
                var mods = new List<string>();
                if (info.PageSize) mods.Add("awem.gridPageSize");
                if (info.PageInfo) mods.Add("awem.gridPageInfo");
                if (info.InfiniteScroll) mods.Add("awem.gridInfScroll");
                grid.Mod(mods.ToArray());
            }

            return grid;
        }
    }
}