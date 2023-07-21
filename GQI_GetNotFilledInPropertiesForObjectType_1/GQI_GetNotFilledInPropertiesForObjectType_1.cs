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

11/07/2023	1.0.0.1		TVD, Skyline	Initial version
****************************************************************************
*/

namespace GQI_GetNotFilledInPropertiesForObjectType_1
{
	using System.Collections.Generic;
	using System.Linq;
	using PropertyRetrieval.Connection;
	using PropertyRetrieval.ItemTypes;
	using Skyline.DataMiner.Analytics.GenericInterface;
	using Skyline.DataMiner.Net;

	[GQIMetaData(Name = "Not Filled In Properties")]
	public class NotFilledInProperties : IGQIDataSource, IGQIOnInit, IGQIInputArguments
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

            columns.Add(new GQIStringColumn("Id"));
            columns.Add(new GQIStringColumn("Name"));

            foreach (var propName in itemTypes.PropertyNamesAndIds.Select(x=> x.Name))
            {
                columns.Add(new GQIStringColumn(propName));
            }

            return columns.ToArray();
        }

        public GQIPage GetNextPage(GetNextPageInputArgs args)
        {
            var rows = new List<GQIRow>();

            foreach (var itemType in itemTypes.PropertiesTable)
            {
                List<GQICell> gqiCells = new List<GQICell>();

                gqiCells.Add(new GQICell() { Value = itemType["Id"] });
                gqiCells.Add(new GQICell() { Value = itemType["Name"] });

                foreach (var propName in itemTypes.PropertyNamesAndIds.Select(x => x.Name))
                {
                    gqiCells.Add(new GQICell() { Value = itemType[propName] });
                }

                rows.Add(new GQIRow(gqiCells.ToArray()));
            }

            return new GQIPage(rows.ToArray())
            {
                HasNextPage = false,
            };
        }
    }
}