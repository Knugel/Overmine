using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Thor;

namespace Overmine.Patches
{
    public class CollectorRoomPatches
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(CollectorRoom), "PopulateFamiliars")]
        public static IEnumerable<CodeInstruction> OnPopulateFamiliars(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var matcher = new CodeMatcher(instructions, il)
                .MatchForward(true, new CodeMatch(i => i.opcode == OpCodes.Blt));

            // Patches the loop so it stops after all the vanilla familiars.
            var ret = matcher
                .Advance(-3)
                .RemoveInstructions(3)
                .InsertAndAdvance(new CodeInstruction(OpCodes.Ldc_I4, 13))
                .InstructionEnumeration();
            
            return ret;
        }
    }
}