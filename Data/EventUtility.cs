using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace A_Winform_Library.Data
{
    /// <summary>
    /// This helper class will removes all events attached to an object
    /// </summary>
    public static class EventUtility
    {
        private static Dictionary<Type, List<FieldInfo>> Event_FieldInfo = new Dictionary<Type, List<FieldInfo>>();

        public static void RemoveAllEventHandlers(object obj)
        {
            RemoveEventHandler(obj, "");
        }

        public static void RemoveEventHandler(object obj, string event_string)
        {
            if (obj == null)
            {
                return;
            }

            Type declaring_type = obj.GetType();
            List<FieldInfo> event_fields = GetTypeEventFields(declaring_type);
            EventHandlerList static_event_handlers = null;

            foreach (FieldInfo field_info in event_fields)
            {
                if (event_string != ""
                    && string.Compare(event_string, field_info.Name, true) != 0)
                {
                    continue;
                }

                if (field_info.IsStatic)
                {
                    if (static_event_handlers == null)
                    {
                        static_event_handlers = GetStaticEventHandlerList(declaring_type, obj);
                    }

                    object idx = field_info.GetValue(obj);
                    Delegate event_delegate = static_event_handlers[idx];
                    if (event_delegate == null) continue;

                    Delegate[] delegates = event_delegate.GetInvocationList();
                    if (delegates == null) continue;

                    EventInfo event_info = declaring_type.GetEvent(field_info.Name, AllBindings);
                    foreach (Delegate del in delegates)
                    {
                        event_info.RemoveEventHandler(obj, del);
                    }
                }
                else
                {
                    EventInfo event_info = declaring_type.GetEvent(field_info.Name, AllBindings);

                    if (event_info != null)
                    {
                        object val = field_info.GetValue(obj);

                        Delegate obj_delegate = (val as Delegate);

                        if (obj_delegate != null)
                        {
                            foreach (Delegate del in obj_delegate.GetInvocationList())
                            {
                                event_info.RemoveEventHandler(obj, del);
                            }
                        }
                    }
                }
            }
        }

        private static BindingFlags AllBindings
        {
            get { return BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static; }
        }

        private static List<FieldInfo> GetTypeEventFields(Type declaring_type)
        {
            if (Event_FieldInfo.ContainsKey(declaring_type))
            {
                return Event_FieldInfo[declaring_type];
            }

            List<FieldInfo> field_infos = new List<FieldInfo>();

            BuildEventFields(declaring_type, field_infos);

            Event_FieldInfo.Add(declaring_type, field_infos);

            return field_infos;
        }

        private static void BuildEventFields(Type t, List<FieldInfo> field_infos)
        {
            Type declaring_type;
            FieldInfo field_info;

            foreach (EventInfo event_info in t.GetEvents(AllBindings))
            {
                declaring_type = event_info.DeclaringType;

                field_info = declaring_type.GetField(event_info.Name, AllBindings);

                if (field_info != null)
                {
                    field_infos.Add(field_info);
                }
            }
        }

        private static EventHandlerList GetStaticEventHandlerList(Type declaring_type, object obj)
        {
            MethodInfo method = declaring_type.GetMethod("get_Events", AllBindings);

            return (EventHandlerList)method.Invoke(obj, new object[] { });
        }
    }
}