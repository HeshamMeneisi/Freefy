﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Freefy
{
    static class Extensions
    {
        public static Delegate[] DisableEvents(this Control ctrl, string eventName)
        {
            PropertyInfo propertyInfo = ctrl.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            EventHandlerList eventHandlerList = propertyInfo.GetValue(ctrl, new object[] { }) as EventHandlerList;
            FieldInfo fieldInfo = typeof(Control).GetField("Event" + eventName, BindingFlags.NonPublic | BindingFlags.Static);

            object eventKey = fieldInfo.GetValue(ctrl);
            var eventHandler = eventHandlerList[eventKey] as Delegate;
            Delegate[] invocationList = eventHandler.GetInvocationList();
            foreach (EventHandler item in invocationList)
            {
                ctrl.GetType().GetEvent(eventName).RemoveEventHandler(ctrl, item);
            }
            return invocationList;
        }
    }
}
