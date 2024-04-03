namespace Goke.Web.Shared.Models
{
    public enum ApparatusItem
    {
        None,
        Beaker,
        MeasuringCylinder,
        RoundBottomFlask,
        RetortStand,
        BunsenBurner,
        ConicalFlask,
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
    }
}
