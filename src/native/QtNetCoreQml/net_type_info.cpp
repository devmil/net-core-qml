#include "net_type_info.h"
#include <QDebug>

NetTypeInfo::NetTypeInfo(std::string fullTypeName) :
    prefVariantType(NetVariantTypeEnum_Invalid),
    fullTypeName(fullTypeName),
    metaObject(NULL)
{

}

NetTypeInfo::~NetTypeInfo()
{

}

NetVariantTypeEnum NetTypeInfo::GetPrefVariantType()
{
    return prefVariantType;
}

void NetTypeInfo::SetPrefVariantType(NetVariantTypeEnum value)
{
    prefVariantType = value;
}

std::string NetTypeInfo::GetFullTypeName()
{
    return fullTypeName;
}

void NetTypeInfo::SetClassName(std::string className)
{
    this->className = className;
}

std::string NetTypeInfo::GetClassName()
{
    return className;
}

void NetTypeInfo::AddMethod(NetMethodInfo* methodInfo)
{
    methods.append(methodInfo);
}

int NetTypeInfo::GetMethodCount()
{
    return methods.length();
}

NetMethodInfo* NetTypeInfo::GetMethod(int index)
{
    if(index < 0) return NULL;
    if(index >= methods.length()) return NULL;

    return methods.at(index);
}

void NetTypeInfo::AddProperty(NetPropertyInfo* propertyInfo)
{
    properties.append(propertyInfo);
}

int NetTypeInfo::GetPropertyCount()
{
    return properties.length();
}

NetPropertyInfo* NetTypeInfo::GetProperty(int index)
{
    if(index < 0) return NULL;
    if(index >= properties.length()) return NULL;

    return properties.at(index);
}

void NetTypeInfo::NotifyPropertyChanged(void* instance, std::string propertyName)
{
    qDebug() << "Got a property changed notification for a " << fullTypeName.c_str() << ". Property = " << propertyName.c_str() << ". Instance = " << reinterpret_cast<uint64_t>(instance);
}
