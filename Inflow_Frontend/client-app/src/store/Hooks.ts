import {TypedUseSelectorHook, useDispatch, useSelector} from "react-redux";
import type {InflowAppDispatch, RootState} from "./Store";

export const useInflowAppDispatch = () => useDispatch<InflowAppDispatch>();
export const useInflowAppSelector: TypedUseSelectorHook<RootState> = useSelector;