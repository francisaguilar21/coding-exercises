using System;
using System.Linq;
using System.Collections.Generic;

namespace CheeseMongers
{
    public class Program
    {
        private IList<CheeseMongersItem> Items;
        public Program(IList<CheeseMongersItem> items)
        {
            Items = items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                switch (Items[i])
                {
                    case ParmigianoRegiano p:
                        Items[i].UpdateQuality();
                        break;
                    case TastingWithChefMassimo t:
                        Items[i].UpdateQuality();
                        break;
                    case CaciocavalloPodolico c:
                        Items[i].UpdateQuality();
                        break;
                    case Ricotta r:
                        Items[i].UpdateQuality();
                        break;
                    default:
                        break;
                }
            }
        }

    }

    public class CheeseMongersItem
    {
        protected const int MIN_QUALITY = 0;
        protected const int MAX_QUALITY = 100;
        public string Name { get; set; }

        public virtual int ValidByDays { get; set; }

        public virtual int Quality { get; set; }

        protected virtual int DepreciationRate { get; set; } = 5;

        public virtual void UpdateQuality()
        {
            // Expired cheese depreciates five times faster
            if (ValidByDays == 0)
            {
                // Quality can never be below 0
                Quality = Quality - (1 * DepreciationRate);

                if (Quality <= 0)
                {
                    Quality = MIN_QUALITY;
                }

                if (Quality >= 100)
                {
                    Quality = MAX_QUALITY;
                }    
            }

            ValidByDays--;
        }
    }

    public class ParmigianoRegiano : CheeseMongersItem
    {
        public ParmigianoRegiano()
        {
            Name = "Parmigiano Regiano";
        }

        public override void UpdateQuality()
        {
            DepreciationRate = -1;
            base.UpdateQuality();
        }
    }

    public class TastingWithChefMassimo : CheeseMongersItem
    {
        public TastingWithChefMassimo()
        {
            Name = "Tasting with Chef Massimo";
        }

        public override void UpdateQuality()
        {
            if (ValidByDays < 15)
            {
                if (Quality < MAX_QUALITY)
                {
                    if (Quality + 2 <= MAX_QUALITY)
                    {
                        Quality = Quality + 2;
                    }
                    else
                    {
                        Quality = MAX_QUALITY;
                    }
                }
            }

            if (ValidByDays < 8)
            {
                if (Quality < MAX_QUALITY)
                {
                    if (Quality + 2 <= MAX_QUALITY)
                    {
                        Quality = Quality + 2;
                    }
                    else
                    {
                        Quality = MAX_QUALITY;
                    }
                }
            }

            if (ValidByDays == 0)
            {
                Quality = MIN_QUALITY;
            }
        }
    }

    public class CaciocavalloPodolico : CheeseMongersItem
    {
        public CaciocavalloPodolico()
        {
            Name = "Caciocavallo Podolico";
        }

        public override void UpdateQuality()
        {
            base.UpdateQuality();
        }
    }

    public class Ricotta : CheeseMongersItem
    {
        public Ricotta()
        {
            Name = "Ricotta";
        }

        public override void UpdateQuality()
        {
            DepreciationRate = DepreciationRate * 3;
            base.UpdateQuality();
        }
    }
}
