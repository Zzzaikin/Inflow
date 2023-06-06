import {createSlice} from "@reduxjs/toolkit";
import ISectionsConfig from "./ISectionsConfig";
import {DATA} from "../../GlobalConsts";
import * as DataService from '../../services/DataService';

const initialState = {
    promiseValue: DataService.selectAsync({
    columnNames: [
        "Id",
        "Name",
        "Image"
    ],
    entityName: "SysSection",
    joins: [
        {
            joinedEntityName: "INFORMATION_SCHEMA.TABLES",
            rightColumnName: "INFORMATION_SCHEMA.TABLES.TABLE_NAME",
            leftColumnName: "SysSection.Name",
            type: DATA.JOIN_TYPE.INNER
        }
    ],
    order: {
        mode: DATA.ORDER_MODE.ASC,
        orderColumnName: "SysSection.Name"
    }
}),
} as ISectionsConfig;

export const SectionsConfigSlice = createSlice({
    name: 'SectionsConfig',
    initialState,
    reducers: {
        sectionsConfig: () => {}
    }
});

export default SectionsConfigSlice.reducer;

export const {sectionsConfig} = SectionsConfigSlice.actions;