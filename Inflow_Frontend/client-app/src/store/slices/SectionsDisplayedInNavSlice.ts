import {createSlice} from "@reduxjs/toolkit";
import * as DataService from '../../services/DataService';
import ISectionsDisplayedInNav from "./ISectionsDisplayedInNav";

const initialState = {
    promiseValue: DataService.selectAsync({
    columnNames: [
        "Id",
        "Name",
        "Image"
    ],
    entityName: "SectionsDisplayedInNav"
}),
} as ISectionsDisplayedInNav;

export const SectionsDisplayedInNavSlice = createSlice({
    name: 'SectionsDisplayedInNav',
    initialState,
    reducers: {
        sectionsDisplayedInNav: () => {}
    }
});

export default SectionsDisplayedInNavSlice.reducer;

export const {sectionsDisplayedInNav} = SectionsDisplayedInNavSlice.actions;