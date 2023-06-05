import {configureStore} from "@reduxjs/toolkit";
import SectionsDisplayedInNavReducer from './slices/SectionsDisplayedInNavSlice';

const appStore = configureStore({
    reducer: {
        SectionsDisplayedInNavPromise: SectionsDisplayedInNavReducer
    }
});

export type RootState = ReturnType<typeof appStore.getState>
export type InflowAppDispatch = typeof appStore.dispatch;
export default appStore;