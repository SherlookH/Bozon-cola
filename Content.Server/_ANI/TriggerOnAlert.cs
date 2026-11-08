
using Content.Server.AlertLevel;
using Content.Shared.DeviceLinking;
using Content.Shared.Trigger;
using Content.Shared.Trigger.Systems;


namespace Content.Server._ANI;

public sealed partial class TriggerOnAlertChangeSystem : TriggerOnXSystem
{
    [Dependency] private readonly TriggerSystem _trigger = default!;
    [Dependency] private readonly SharedDeviceLinkSystem _deviceLink = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AlertLevelChangedEvent>(OnAlertChange);
        SubscribeLocalEvent<TriggerOnAlertChangeComponent, MapInitEvent>(OnMapInit);
    }

    private void OnAlertChange(AlertLevelChangedEvent args)
    {
        var query = EntityQueryEnumerator<TriggerOnAlertChangeComponent>();
        while (query.MoveNext(out var ent, out var component))
        {
            if (component.NeedAlert.Contains(args.AlertLevel))
            {
                _deviceLink.InvokePort(ent, "CodeSet");
            }
            else
            {
                _deviceLink.InvokePort(ent, "CodeChange");
            }

        }
    }

    private void OnMapInit(EntityUid uid, TriggerOnAlertChangeComponent comp, MapInitEvent args)
    {
        _deviceLink.EnsureSourcePorts(uid, "CodeSet", "CodeChange");

    }


}
