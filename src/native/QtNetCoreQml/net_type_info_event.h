#ifndef NET_TYPE_INFO_EVENT_H
#define NET_TYPE_INFO_EVENT_H

#include "net_type_info_method.h"

class NetEventInfo : public NetMethodInfo
{
public:
    NetEventInfo(NetTypeInfo *parentTypeInfo, std::string methodName, NetTypeInfo *returnType);
};

#endif // NET_TYPE_INFO_EVENT_H
