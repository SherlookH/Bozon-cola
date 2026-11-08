
using Robust.Shared.Prototypes;
using Content.Server.AlertLevel;

namespace Content.Server._ANI;


[RegisterComponent]
public sealed partial class TriggerOnAlertChangeComponent : Component
{
    [DataField("needAlert"), ViewVariables(VVAccess.ReadWrite)]
    public List<ProtoId<AlertLevelPrototype>> NeedAlert = new() { "green" };
}
