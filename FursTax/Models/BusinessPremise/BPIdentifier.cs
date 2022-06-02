using System;

namespace MadWare.Furs.Models.BusinessPremise
{
    public class BPIdentifier : BaseModel
    {
        public enum PremiseTypeEnum { A, B, C }

        /// <summary>
        /// Vpišejo se podatki o nepremičnem poslovnem prostoru, če zavezanec izdaja račune v nepremičnem poslovnem prostoru. / Data about immovable business premises are entered if the person liable issues invoices in immovable business premises.
        /// </summary>
        public RealEstateBP RealEstateBP { get; set; }

        /// <summary>
        /// Vpiše se vrsta poslovnega prostora, če zavezanec izdaja račune v premičnem poslovnem prostoru / The type of business premises is entered if the person liable issues invoices in movable business premises:
        /// A – premičen objekt(npr.prevozno sredstvo, premična stojnica) ali / movable object (e.g.vehicle, movable stand) or
        /// B – objekt na stalni lokaciji(npr.stojnica na tržnici, kiosk) ali / object at a permanent location(e.g.market stand, newsstand) or
        /// C – posamezna elektronska naprava za izdajo računov ali vezana knjiga računov v primerih, ko zavezanec ne uporablja drugega poslovnega prostora / individual electronic device for issuing invoices or pre-numbered invoice book in cases when the person liable doesn't use other business premises
        /// </summary>
        public PremiseTypeEnum? PremiseType { get; set; }

        public bool ShouldSerializeRealEstateBP()
        {
            return this.RealEstateBP != null;
        }

        public bool ShouldSerializePremiseType()
        {
            return this.PremiseType.HasValue;
        }

        public override void Validate()
        {
            base.Validate();

            if (this.RealEstateBP != null && this.PremiseType != null)
                throw new Exception("RealEstateBP and PremiseType can not both be included in BPIdentifier.");

            if (this.RealEstateBP == null && this.PremiseType == null)
                throw new Exception("RealEstateBP and PremiseType can not both be null in BPIdentifier.");

            if (this.RealEstateBP != null)
                this.RealEstateBP.Validate();
        }
    }
}