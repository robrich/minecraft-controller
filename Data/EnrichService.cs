using System.Collections.Generic;
using System.Linq;
using MinecraftController.Data.Models;

namespace MinecraftController.Data {
    public class EnrichService {

        /// <summary>
        /// Rename things to things that makes sense to my kids<br />
        /// TODO: find a way to store this in config
        /// </summary>
        public List<AzureInfo> AddData(List<AzureInfo> Servers) {

            AzureInfo homecraft = Servers.First(s => s.Name == "homecraft");
            homecraft.NickName = "Survival";
            homecraft.Url = "minecraft.robrich.org";
            homecraft.FadeDirection = "fade-right";

            AzureInfo homecraft2 = Servers.First(s => s.Name == "homecraft2");
            homecraft2.NickName = "Machines";
            homecraft2.Url = "minecraft-boys.robrich.org";
            homecraft2.FadeDirection = "fade";

            AzureInfo homecraft3 = Servers.First(s => s.Name == "homecraft3");
            homecraft3.NickName = "Roleplay";
            homecraft3.Url = "minecraft-girls.robrich.org";
            homecraft3.FadeDirection = "fade-left";
            
            return new List<AzureInfo> {
                homecraft, homecraft2, homecraft3
            };
        }

    }
}
