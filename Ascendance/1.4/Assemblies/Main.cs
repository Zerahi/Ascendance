using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Ascendance {
    public class Recipe_RegrowthHediff : Recipe_Surgery {
        public virtual IEnumerable<BodyPartRecord> GetPartsToApplyOn( Pawn pawn, RecipeDef recipe) {
            List<Hediff> allHediffs = pawn.health.hediffSet.hediffs;
            for (int i = 0; i < allHediffs.Count; i++) {
                if (allHediffs[i].Part != null && allHediffs[i].Part.def != BodyPartDefOf.Brain && HediffUtility.TryGetComp<HediffComp_GetsPermanent>(allHediffs[i]) != null && HediffUtility.IsPermanent(allHediffs[i]))
                    yield return allHediffs[i].Part;
            }
        }

        public virtual void ApplyOnPawn( Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill) {
            Hediff hediff = pawn.health.hediffSet.hediffs.Find((Predicate<Hediff>)(x => HediffUtility.TryGetComp<HediffComp_GetsPermanent>(x) != null && x.Part == part && HediffUtility.IsPermanent(x)));
            if (billDoer == null || this.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                return;
            pawn.health.RemoveHediff(hediff);
        }
    }
}
