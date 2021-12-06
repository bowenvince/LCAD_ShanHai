using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DialogNodeVisitor
{
    void Visit(DialogNode node);
    void Visit(DialogChoiceNode node);
}
