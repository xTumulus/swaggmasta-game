using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGadget {
    bool GadgetUse(Swagg tempSwagg, System.Func<IGadget, bool> tempCallback );
    bool GadgetCancel();
    string name { get; }
}
