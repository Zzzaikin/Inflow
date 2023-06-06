import {configureStore} from "@reduxjs/toolkit";
import SectionsConfigReducer from './slices/SectionsConfigSlice';

const appStore = configureStore({
    reducer: {
        SectionsConfigPromise: SectionsConfigReducer
    }
});

export type RootState = ReturnType<typeof appStore.getState>
export type InflowAppDispatch = typeof appStore.dispatch;
export default appStore;