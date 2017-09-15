#include "net_type_info_event.h"

NetEventInfo::NetEventInfo(NetTypeInfo *parentTypeInfo, std::string methodName, NetTypeInfo *returnType)
    : NetMethodInfo(parentTypeInfo, methodName, returnType)
{

}
