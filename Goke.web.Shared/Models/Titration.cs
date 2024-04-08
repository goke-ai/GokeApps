namespace Goke.Web.Shared.Models
{
    public class Titration
    {
        public List<Apparatus> Apparatuses { get; }

        public List<List<ApparatusItem>> Sequences => [
                // [ApparatusItem.Titration],
                [ApparatusItem.Beaker,  ApparatusItem.Alkali, ApparatusItem.Pipette, ApparatusItem.ConicalFlask, ApparatusItem.PHIndicator],
                [ApparatusItem.MeasuringCylinder,  ApparatusItem.Acid],
                [ApparatusItem.RetortStand, ApparatusItem.Burette, ApparatusItem.MeasuringCylinder, ApparatusItem.ConicalFlask],
                [ApparatusItem.Titration],
            ];

        public Titration()
        {
            Apparatuses = new()
            {
                // [ApparatusItem.Beaker,  ApparatusItem.Alkali, ApparatusItem.Pipette, ApparatusItem.ConicalFlask, ApparatusItem.PHIndicator],
                new Apparatus{Item  = ApparatusItem.Beaker, Media = "images/apparatus/beaker.png", Title = "Beaker",
                Action = () => { return "Beaker";},
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.None == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return @"<video width='100%' autoplay playsinline muted>
                                                        <source src=""videos/beaker.mp4"" type=""video/mp4"">
                                                        <source src=""videos/beaker.m4v"" type=""video/x-m4v"">
                                                        Your browser does not support the video tag.                                                        
                                                    </video>"; };
                    }
                }},
                new Apparatus{Item  = ApparatusItem.Alkali, Media = "images/apparatus/alkali.png", Title = "Alkali",
                Action = () => { return "Alkali"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.Beaker == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        // y.Action = () => { return "Pour Alkali into Beaker"; };
                        y.Action = () => { return @"<video width='100%' autoplay playsinline muted>
                                                        <source src=""videos/alkali.mp4"" type=""video/mp4"">
                                                        <source src=""videos/alkali.m4v"" type=""video/x-m4v"">
                                                        Your browser does not support the video tag.
                                                    </video>"; };

                        var x = list.First(w => w.Item == ApparatusItem.Beaker);
                        x.Title = "Beaker + Alkali";
                        //x.Media = "images/apparatus/beaker2.png";
                        //x.Action = () => { return "Put filled Beaker on the table";};
                    }
                }},
                new Apparatus{Item  = ApparatusItem.Pipette, Media = "images/apparatus/pipette.png", Title = "Pipette",
                Action = () => { return "Pipette"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.Alkali == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        // y.Action = () => { return "Pipette draw content from Beaker"; };
                        y.Action = () => { return @"<video width='100%' autoplay playsinline muted>
                                                        <source src=""videos/pipette.mp4"" type=""video/mp4"">
                                                        Your browser does not support the video tag.
                                                    </video>"; };
                    }
                }},
                new Apparatus{Item  = ApparatusItem.ConicalFlask, Media = "images/apparatus/conical-flask.png", Title = "Conical Flask",
                Action = () => { return "ConicalFlask"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.Pipette == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Title = "Conical Flask + Alkali";
                        // y.Action = () => { return "Pour Pipette content into Conical-Flask"; };
                        y.Action = () => { return @"<video width='100%' autoplay playsinline muted>
                                                        <source src=""videos/conical-flask.mp4"" type=""video/mp4"">
                                                        Your browser does not support the video tag.
                                                    </video>"; };
                    }
                    if(ApparatusItem.MeasuringCylinder == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return "Put Conical-Flask under Burette"; };
                    }
                }},
                new Apparatus{Item  = ApparatusItem.PHIndicator, Media = "images/apparatus/ph-indicator.png", Title = "pH Indicator",
                Action = () => { return "PHIndicator"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.ConicalFlask == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return "pH Indicator drop into Conical-Flask"; };
                        y.Action = () => { return @"<video width='100%' autoplay playsinline muted>
                                                        <source src=""videos/ph-indicator.mp4"" type=""video/mp4"">
                                                        Your browser does not support the video tag.
                                                    </video>"; };

                        var x = list.First(w => w.Item == ApparatusItem.ConicalFlask);
                        x.Title = "Conical Flask + Alkali + Indicator";
                        // x.Media = "images/apparatus/beaker2.png";
                        //x.Action = () => { return "Put filled Beaker on the table";};
                    }
                }},

                // [ApparatusItem.MeasuringCylinder,  ApparatusItem.Acid],
                new Apparatus{Item  = ApparatusItem.MeasuringCylinder, Media = "images/apparatus/measuring-cylinder.png", Title = "Measuring Cylinder",
                Action = () => { return "MeasuringCylinder"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.None == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return "Put Measuring-Cylinder on the table"; };
                    }

                    if(ApparatusItem.Burette == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return "Pour Acid into the Burette"; };
                        y.Title = "Measuring Cylinder";

                        var x = list.First(w => w.Item == ApparatusItem.Burette);
                        x.Title = "Burette + Acid";
                        // x.Media = "images/apparatus/beaker2.png";
                        //x.Action = () => { return "Put filled Beaker on the table";};
                    }
                }},
                new Apparatus{Item  = ApparatusItem.Acid, Media = "images/apparatus/acid.png", Title = "Acid",
                Action = () => { return "Acid"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.MeasuringCylinder == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return "Pour Acid into the Measuring-Cylinder"; };

                        var x = list.First(w => w.Item == ApparatusItem.MeasuringCylinder);
                        x.Title = "Measuring Cylinder + Acid";
                        // x.Media = "images/apparatus/beaker2.png";
                        //x.Action = () => { return "Put filled Beaker on the table";};
                    }
                }},


                // [ApparatusItem.RetortStand, ApparatusItem.Burette, ApparatusItem.MeasuringCylinder, ApparatusItem.ConicalFlask],
                new Apparatus{Item  = ApparatusItem.RetortStand, Media = "images/apparatus/retort-stand.png", Title = "Retort Stand",
                Action = () => { return "RetortStand"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.None == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Action = () => { return "Put Retort-Stand on the table"; };
                    }
                }},
                new Apparatus{Item  = ApparatusItem.Burette, Media = "images/apparatus/burette.png", Title = "Burette",
                Action = () => { return "Burette"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.RetortStand == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        y.Title = "Mounted Burette";
                        y.Action = () => { return "Mount the Burette on Retort-Stand"; };
                    }
                }},


                // [ApparatusItem.Titration]
                new Apparatus{Item  = ApparatusItem.Titration, Media = "images/apparatus/titration.png", Title = "Titration",
                Action = () => { return "Titration"; },
                Update = (List<Apparatus> list, ApparatusItem currentItem, ApparatusItem previousItem) => {
                    if(ApparatusItem.None == previousItem)
                    {
                        var y = list.First(w => w.Item == currentItem);
                        //y.Title = "Mounted Burette";
                        y.Action = () => { return @"Press the START to begin Titration"; };
                    }
                }},

                new Apparatus{Item  = ApparatusItem.RoundBottomFlask, Media = "images/apparatus/round-bottom-flask.png", Title = "Round Bottom Flask",
                Action = () => { return "RoundBottomFlask"; },
                },
                new Apparatus{Item  = ApparatusItem.BunsenBurner, Media = "images/apparatus/bunsen-burner.png", Title = "Bunsen Burner", Action = () => { return "BunsenBurner"; }, },
                new Apparatus{Item  = ApparatusItem.Water, Media = "images/apparatus/water.png", Title = "Water", Action = () => { return "Water"; }, },
            };

        }

    }
}
