%{
#include "net_type_info_event.h"
%}

class NetEventInfo : public NetMethodInfo
{
public:
    NetEventInfo(NetTypeInfo *parentTypeInfo, std::string methodName, NetTypeInfo *returnType);
};