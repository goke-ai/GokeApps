namespace Goke.Web.Shared.Models
{
    public enum ApparatusItem
    {
        None,
        Beaker, Beaker1, Beaker2,
        MeasuringCylinder,
        RoundBottomFlask, RoundBottomFlask1, RoundBottomFlask2,
        RetortStand,
        BunsenBurner,
        ConicalFlask, ConicalFlask1, ConicalFlask2,
        Pipette,
        Burette,
        TripodStand,
        Acid,
        Alkali,
        Water,
        PHIndicator,
        Titration,
    }

    public class Apparatus
    {
        public ApparatusItem Item { get; set; } = ApparatusItem.None;
        public string Media { get; set; } = "images/apparatus/beaker.png";
        public string Title { get; set; } = "Beaker";
        public Func<string> Action { get; set; } = () => { return "Beaker"; };
        public Action<List<Apparatus>, ApparatusItem, ApparatusItem>? Update { get; set; }
        public int MinTimeInSeconds { get; set; } = 10;
    }
}
