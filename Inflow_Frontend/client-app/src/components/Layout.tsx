import React, {JSX, RefObject, useEffect, useState} from 'react';
import {useInflowAppSelector} from "../store/Hooks";

import {Route, Routes} from "react-router-dom";
import Section from "./Section/Section";

import './Layout.scss';

export const LAYOUT_REF: RefObject<any> = React.createRef();

function Layout() {
    const [routes, setRoutes] = useState<JSX.Element[]>([]);

    const sectionsConfigPromise: Promise<[]> = useInflowAppSelector((state) => {
        return state.SectionsConfigPromise.promiseValue;
    });

    useEffect(() => {
        sectionsConfigPromise
            .then((sectionsConfig: {Name: string, LczName: string}[]) => {
                const routes = getRoutesBySectionsConfig(sectionsConfig);
                setRoutes(routes);
            });
    }, []);

    function getRoutesBySectionsConfig(sectionsConfig: {Name: string, LczName: string}[]): JSX.Element[] {
        const routes = sectionsConfig.map((sectionsConfigItem: {Name: string, LczName: string}) => {
            return <Route
                        path={sectionsConfigItem.Name}
                        element={<Section testText={sectionsConfigItem.LczName} />}
                   />
            });

        const rootRoute =
            <Route path="/" element={<Section testText={sectionsConfig[0].LczName} />}/>;

        routes.push(rootRoute);
        return routes;
    }

    return (
        <div ref={LAYOUT_REF} className="layout">
            <Routes>
                {routes}
            </Routes>
        </div>
    );
}

export default Layout;