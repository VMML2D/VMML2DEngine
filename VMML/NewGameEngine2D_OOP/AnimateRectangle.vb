Public Structure AnimateRectangleInStack
    Private ValueOfRec As Integer
    ''' <summary>
    ''' Vergrößert jeweils den X,Y-Wert um den angegeben Expansewert.
    ''' </summary>
    ''' <param name="AnimateRectangleInStack"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator +(AnimateRectangleInStack As AnimateRectangleInStack, Value As Integer) As Integer
        AnimateRectangleInStack.ValueOfRec = AnimateRectangleInStack.ValueOfRec + Value
        Return AnimateRectangleInStack.ValueOfRec
    End Operator
    Public Shared Operator -(AnimateRectangleInStack As AnimateRectangleInStack, Value As Integer) As Integer
        AnimateRectangleInStack.ValueOfRec -= Value
        Return AnimateRectangleInStack.ValueOfRec
    End Operator
    Public Shared Operator *(AnimateRectangleInStack As AnimateRectangleInStack, Value As Integer) As Integer
        AnimateRectangleInStack.ValueOfRec *= Value
        Return AnimateRectangleInStack.ValueOfRec
    End Operator
    Public Shared Operator /(AnimateRectangleInStack As AnimateRectangleInStack, Value As Integer) As Integer
        AnimateRectangleInStack.ValueOfRec = Convert.ToInt32(AnimateRectangleInStack.ValueOfRec / Value)
        Return AnimateRectangleInStack.ValueOfRec
    End Operator
End Structure

