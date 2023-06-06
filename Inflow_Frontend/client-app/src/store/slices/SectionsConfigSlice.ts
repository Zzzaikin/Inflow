import {createSlice} from "@reduxjs/toolkit";
import ISectionsConfig from "./ISectionsConfig";
import {DATA} from "../../GlobalConsts";
import * as DataService from '../../services/DataService';

const initialState = {
    promiseValue: DataService.selectAsync({
    columnNames: [
        "Id",
        "Name",
        "SectionImage"
    ],
    entityName: "SysEntity",
    joins: [
        {
            joinedEntityName: "INFORMATION_SCHEMA.TABLES",
            rightColumnName: "INFORMATION_SCHEMA.TABLES.TABLE_NAME",
            leftColumnName: "SysEntity.Name",
            type: DATA.JOIN_TYPE.INNER
        }
    ],
    order: {
        mode: DATA.ORDER_MODE.ASC,
        orderColumnName: "SysEntity.SectionPositionInNavMenu"
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