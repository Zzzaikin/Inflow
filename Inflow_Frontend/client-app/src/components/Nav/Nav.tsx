import React, {JSX, useEffect, useState} from 'react';
import { Navigate } from 'react-router-dom';

import INavItemConfig from "./INavItemConfig";
import {LAYOUT_REF} from "../Layout";
import {useInflowAppSelector} from "../../store/Hooks";
import NavItem from './NavItem';

import './Nav.scss'

function Nav() {
    const navComponentClassNameText : string = "nav-component";
    const navComponentCollapsedClassNameText : string = "nav-component collapsed";

    const [toggleButtonText, setToggleButtonText] = useState("<");
    const [navComponentClassName, setNavComponentClassName] = useState("nav-component");
    const [navigate, setNavigate] = useState<JSX.Element>();

    const [navItems, setNavItems] = useState<JSX.Element[]>([]);

    const sectionsDisplayedInNavConfigPromise: Promise<[]> = useInflowAppSelector((state) => {
        return state.SectionsDisplayedInNavPromise.promiseValue;
    });

    let navItemsConfig: INavItemConfig[] = [];

    useEffect(() => {
        sectionsDisplayedInNavConfigPromise
            .then((sectionsDisplayedInNavConfig) => {
                navItemsConfig = sectionsDisplayedInNavConfig.map((sectionConfig: INavItemConfig) => {
                    return {
                        ...sectionConfig,
                        selected: false
                    }
                });

                navItemsConfig[0].Selected = true;

                const navItemsFromSectionConfigs = getNavItemsFromConfig(navItemsConfig);
                setNavItems(navItemsFromSectionConfigs);
            });
    }, []);

    function getNavItemsFromConfig(config: INavItemConfig[]) : JSX.Element[] {
        const navItems: JSX.Element[] = config.map((configItem: INavItemConfig) => {
            return(
                <div
                    key={`${configItem.Id}`}
                    id={`${configItem.Id}`}
                    className={configItem.Selected ? "nav-item selected" : "nav-item"}
                    onClick={(e: any) => onNavItemClick(e)}
                >
                    <NavItem text={configItem.Name} image={configItem.Image} />
                </div>
            );
        });

        return navItems;
    }

    function onNavItemClick(e: any) {
        updateNavItemsClickedState(e);
        navigateToSection(e);
    }

    function navigateToSection(e: any) {
        const navItemConfigItemHasFoundById = navItemsConfig.find((navItemConfigItem) => {
            const target = e.target;
            const navItemConfigItemId = navItemConfigItem.Id;

            return (navItemConfigItemId === target.id) || (navItemConfigItemId === target.parentElement.id);
        });

        const navigateComponent = <Navigate to={`/${navItemConfigItemHasFoundById?.Name}`}/>;
        setNavigate(navigateComponent);
    }

    function updateNavItemsClickedState(e: any) {
        navItemsConfig.forEach((navItemConfig: INavItemConfig, i: number, arr: any[]) => {
            if (navItemConfig.Selected) {
                arr[i].Selected = false
            }

            const target = e.target;

            if (navItemConfig.Id === target.id) {
                arr[i].Selected = true;
                return;
            }

            if (navItemConfig.Id === target.parentElement.id) {
                arr[i].Selected = true;
            }
        });

        const updatedNavItems = getNavItemsFromConfig(navItemsConfig);

        /*
         * TODO: Очень плохая реализация. Перерисовываются все NavItem'ы, что в будущем аукнется, но сейчам нет времени.
         * Необходимо перерисовывать только выбранный NavItem и выбранный ранее.
         * UPD: возможно стоит попробовать в redux состояние navitem'ов запихнуть.
         */
        setNavItems(updatedNavItems);
    }

    function onToggleButtonClick() {
        const navClassName : string =
            navComponentClassName === navComponentClassNameText
                ? navComponentCollapsedClassNameText
                : navComponentClassNameText;

        setNavComponentClassName(navClassName);

        LAYOUT_REF.current.classList.toggle("nav-collapsed");

        const toggleText : string = toggleButtonText === "<" ? ">" : "<";
        setToggleButtonText(toggleText);
    }

    return(
        <div className={navComponentClassName}>
            <div className="inflow-label-container">
                <div className="inflow-label">Inflow</div>
            </div>
            <div className="nav-items-container">
                {navItems}
            </div>
            <div className="nav-controls">
                <div className="user-profile" />
                <div
                    className="toggle-button"
                    onClick={() => onToggleButtonClick()}
                >{toggleButtonText}</div>
            </div>
            {navigate}
        </div>
    );
}

export default Nav;