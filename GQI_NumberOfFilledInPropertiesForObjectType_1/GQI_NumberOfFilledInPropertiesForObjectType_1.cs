/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

12/07/2023	1.0.0.1		TVD, Skyline	Initial version
****************************************************************************
*/

namespace GQI_NumberOfFilledInPropertiesForObjectType_1
{
    using System;
    using System.Collections.Generic;
    using PropertyRetrieval.Connection;
    using PropertyRetrieval.ItemTypes;
    using Skyline.DataMiner.Analytics.GenericInterface;
    using Skyline.DataMiner.Net;

    [GQIMetaData(Name = "Number Of Not Filled In Properties")]
    public class NumberOfNotFilledInProperties : IGQIDataSource, IGQIOnInit, IGQIInputArguments
    {
        private GQIDMS _dms;
        private GQIStringDropdownArgument _propTypeArgument = new GQIStringDropdownArgument("Property Type", ItemTypeHelper.GetItemTypes()) { IsRequired = true };
        private string _propType;
        private IConnection _connection;
        private IitemTypes itemTypes;

        public OnInitOutputArgs OnInit(OnInitInputArgs args)
        {
            _dms = args.DMS;
            _connection = new GQIConnection(_dms);
            return default;
        }

        public GQIArgument[] GetInputArguments()
        {
            return new GQIArgument[] { _propTypeArgument };
        }

        public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
        {
            _propType = args.GetArgumentValue(_propTypeArgument);
            return new OnArgumentsProcessedOutputArgs();
        }

        public GQIColumn[] GetColumns()
        {
            itemTypes = ItemTypeHelper.GetItemTypesFromTypeName(_propType,_connection);

            List<GQIColumn> columns = new List<GQIColumn>();

            columns.Add(new GQIDoubleColumn("Id"));
            columns.Add(new GQIStringColumn("Name"));
            columns.Add(new GQIDoubleColumn("Number of filled in"));
            columns.Add(new GQIDoubleColumn("Number of not filled in"));

            return columns.ToArray();
        }

        public GQIPage GetNextPage(GetNextPageInputArgs args)
        {
            var rows = new List<GQIRow>();

            foreach (var propConfig in itemTypes.PropertyUsages)
            {
                rows.Add(new GQIRow(
                     new GQICell[]
                     {
                          new GQICell() { Value = Convert.ToDouble(propConfig.ConfigInfo.Id) },
                          new GQICell() { Value = propConfig.ConfigInfo.Name },
                          new GQICell() { Value = Convert.ToDouble(propConfig.NrOfFilledIn) },
                          new GQICell() { Value = Convert.ToDouble(propConfig.NrOfNotFilledIn) },
                     }));
            }

            return new GQIPage(rows.ToArray())
            {
                HasNextPage = false,
            };
        }
    }
}